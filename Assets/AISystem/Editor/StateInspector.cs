﻿using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Reflection;
using AISystem;
using AISystem.Actions;
using AISystem.States;

[CustomEditor(typeof(State),true)]
public class StateInspector : Editor {
	private State state;
	private ReorderableList actionList;
	private ReorderableList transitionList;
	private int transitionIndex = 0;
	private ReorderableList conditionList;
	private ReorderableList stateList;
	private FieldInfo[] stateFields;

	private void OnEnable(){
		if (!(target is State)) {
			return;
		}
		state = (State)target;
		stateList = new ReorderableList (state.GetType().ToString().Split('.').Last());
		stateFields = state.GetType ()
			.GetFields (BindingFlags.Public | BindingFlags.Instance)
			.Where (x => x.DeclaringType.IsSubclassOf (typeof(State)))
			.ToArray();

		object[] attributes=state.GetType().GetCustomAttributes(true);
		bool isTrigger=false;
		foreach(object attribute in attributes){
			if(attribute is TriggerAttribute){
				isTrigger=true;
			}
		}

		if (!isTrigger) {
			actionList = new ReorderableList (state.actions, "Actions", true);
			actionList.onAddCallback = AddAction;
			actionList.onRemoveCallback = RemoveAction;
			actionList.drawElementCallback = DrawActionElement;
			actionList.onHeaderClick = OnActionHeaderClick;
		}

		transitionList = new ReorderableList (state.transitions, "Transitions", true,false,false);
		transitionList.onRemoveCallback = RemoveTransition;
		transitionList.drawElementCallback = DrawTransitionElement;
		transitionList.onSelectCallback = TransitionSelected;

	}

	private void OnDestroy(){
		SceneView.onSceneGUIDelegate -= OnSceneView;
	}

	public override void OnInspectorGUI ()
	{
		if (state == null) {
			return;
		}

		if(SceneView.onSceneGUIDelegate != this.OnSceneView && state is AISystem.States.NavMeshAgent.Patrol)
		{
			SceneView.onSceneGUIDelegate += this.OnSceneView;
		}

		if(stateFields.Length>0){
			if (stateList.DoListHeader ()) 
			{
				GUILayout.BeginVertical (GUIContent.none, "PopupCurveSwatchBackground");
				GUILayout.Space(2);
				SerializedObject obj = new SerializedObject (state);
				obj.Update ();
				for (int cnt=0; cnt<stateFields.Length; cnt++) {
					bool hasChildren=obj.FindProperty (stateFields [cnt].Name).CountInProperty()>1;
					if(hasChildren){
						GUILayout.BeginHorizontal();
						GUILayout.Space(15f);
						GUILayout.BeginVertical();
					}
					EditorGUILayout.PropertyField (obj.FindProperty (stateFields [cnt].Name),true);
					if(hasChildren){
						GUILayout.EndVertical();
						GUILayout.EndHorizontal();
					}

				}
				obj.ApplyModifiedProperties ();
				GUILayout.EndVertical();
			}

			GUILayout.Space (10f);
		}

		if (actionList != null) {
			actionList.DoList ();
			GUILayout.Space (10f);
		}

		if (state.transitions.Count > 0) {
			transitionList.DoList ();
			GUILayout.Space(10f);
			if(transitionIndexChanged &&Event.current.type== EventType.Layout){
				conditionList=null;
				transitionIndexChanged=false;
			}
			if(conditionList == null){
				conditionList = new ReorderableList (state.transitions[transitionIndex].conditions,"Conditions",true,true,true);
				conditionList.onAddCallback = AddCondition;
				conditionList.onRemoveCallback = RemoveCondition;
				conditionList.drawElementCallback = DrawConditionElement;
				conditionList.onHeaderClick=OnConditionHeaderClick;
			}
			conditionList.DoList();

		}
	}

	public static State conditionCopiedState;
	public static List<BaseCondition> copiedConditions;
	private void OnConditionHeaderClick(){
		GenericMenu genericMenu = new GenericMenu ();
		if(state.transitions[transitionIndex].conditions.Count == 0){
			genericMenu.AddDisabledItem (new GUIContent ("Copy"));
		}else{
			genericMenu.AddItem (new GUIContent ("Copy"), false, new GenericMenu.MenuFunction (this.CopyConditions));
		}
		if(copiedConditions != null && copiedConditions.Count>0 && conditionCopiedState != null && conditionCopiedState.id != state.id){
			genericMenu.AddItem (new GUIContent ("Paste"), false, new GenericMenu.MenuFunction (this.PasteConditions));
		}else{
			genericMenu.AddDisabledItem (new GUIContent ("Paste"));
		}
		genericMenu.ShowAsContext ();
		Event.current.Use ();
	}


	private void CopyConditions(){
		conditionCopiedState = state;
		copiedConditions = new List<BaseCondition> (state.transitions[transitionIndex].conditions);
	}

	private void PasteConditions(){
		for (int i=0; i< state.transitions[transitionIndex].conditions.Count; i++) {
			DestroyImmediate(state.transitions[transitionIndex].conditions[i],true);
		}
		state.transitions[transitionIndex].conditions.Clear ();
		AssetDatabase.SaveAssets ();
		
		foreach (BaseCondition condition in copiedConditions) {
			if(condition != null){
				BaseCondition copy=(BaseCondition)ScriptableObject.Instantiate(condition);
				AssetDatabase.AddObjectToAsset(copy,state.transitions[transitionIndex]);
				state.transitions[transitionIndex].conditions.Add(copy);
			}
		}
		AssetDatabase.SaveAssets ();
	}

	private void OnActionHeaderClick(){
		GenericMenu genericMenu = new GenericMenu ();
		if(state.actions.Count == 0 ){
			genericMenu.AddDisabledItem (new GUIContent ("Copy"));
		}else{
			genericMenu.AddItem (new GUIContent ("Copy"), false, new GenericMenu.MenuFunction (this.CopyActions));
		}
		if(copiedActions != null && copiedActions.Count>0 && actionsCopiedState != null && actionsCopiedState.id != state.id){
			genericMenu.AddItem (new GUIContent ("Paste"), false, new GenericMenu.MenuFunction (this.PasteActions));
		}else{
			genericMenu.AddDisabledItem (new GUIContent ("Paste"));
		}
		genericMenu.ShowAsContext ();
		Event.current.Use ();
	}

	public static State actionsCopiedState;
	public static List<BaseAction> copiedActions;
	private void CopyActions(){
		actionsCopiedState = state;
		copiedActions = new List<BaseAction> (state.actions);
	}

	private void PasteActions(){

		for (int i=0; i< state.actions.Count; i++) {
			DestroyImmediate(state.actions[i],true);
		}
		state.actions.Clear ();
		AssetDatabase.SaveAssets ();

		foreach (BaseAction action in copiedActions) {
			BaseAction copy=(BaseAction)ScriptableObject.Instantiate(action);
			AssetDatabase.AddObjectToAsset(copy,state);
			state.actions.Add(copy);
		}
		AssetDatabase.SaveAssets ();
	}

	private void AddCondition(){
		GenericMenu genericMenu = new GenericMenu ();
		IEnumerable<Type> types= AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes()) .Where(type => type.IsSubclassOf(typeof(BaseCondition)));
		foreach(Type type in types){
			object[] attributes=type.GetCustomAttributes(true);
			string category=string.Empty;
			foreach(object attribute in attributes){
				if(attribute is CategoryAttribute){
					category=(attribute as CategoryAttribute).Category+"/";
				}
			}
			genericMenu.AddItem (new GUIContent (category+type.ToString().Split('.').Last()), false, new GenericMenu.MenuFunction2 (this.OnCreateCondition),type);
			
		}
		genericMenu.ShowAsContext ();
	}

	
	private void OnCreateCondition(object data){
		BaseCondition condition =(BaseCondition) ScriptableObject.CreateInstance ((Type)data);
		condition.name = condition.GetType ().ToString ().Split ('.').Last ();
		AssetDatabase.AddObjectToAsset (condition, state);
		AssetDatabase.SaveAssets();
		state.transitions[transitionIndex].conditions.Add(condition);
	}

	private void RemoveCondition(int index){
		DestroyImmediate(state.transitions[transitionIndex].conditions[index],true);
		state.transitions[transitionIndex].conditions.RemoveAt(index);
		AssetDatabase.SaveAssets();
	}

	private void DrawConditionElement(int index){
		if (transitionIndexChanged) {
			return;
		}
		BaseCondition condition = state.transitions[transitionIndex].conditions [index];
		FieldInfo[] fields = condition.GetType ().GetFields (BindingFlags.Public | BindingFlags.Instance);
		SerializedObject obj = new SerializedObject (condition);
		obj.Update ();
		for (int cnt=0; cnt<fields.Length; cnt++) {
			SerializedProperty prop=obj.FindProperty (fields [cnt].Name);
			if(prop != null){
				EditorGUILayout.PropertyField (prop);
			}
		}
		obj.ApplyModifiedProperties ();
	}

	private bool transitionIndexChanged;
	private void TransitionSelected(int index){
		transitionIndex = index;
		transitionIndexChanged = true;
		((AIEditorWindow)EditorWindow.GetWindow (typeof(AIEditorWindow))).TransitionIndexChanged(index);
	}

	private void RemoveTransition(int index){
		transitionIndex = 0;
		transitionIndexChanged = true;
		DestroyImmediate(state.transitions[index],true);
		state.transitions.RemoveAt(index);
		AssetDatabase.SaveAssets();
		((AIEditorWindow)EditorWindow.GetWindow (typeof(AIEditorWindow))).TransitionIndexChanged(0);
	}

	private void DrawTransitionElement(int index){
		GUI.color = transitionIndex == index ? EditorStyles.foldout.focused.textColor : Color.white;
		GUILayout.Label (state.transitions [index].fromState.name + " -> " + state.transitions [index].toState.name);
		GUI.color = Color.white;
	}

	private void AddAction(){
		GenericMenu genericMenu = new GenericMenu ();
		IEnumerable<Type> types= AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes()) .Where(type => type.IsSubclassOf(typeof(BaseAction)));
		foreach(Type type in types){
			object[] attributes=type.GetCustomAttributes(true);
			string category=string.Empty;
			foreach(object attribute in attributes){
				if(attribute is CategoryAttribute){
					category=(attribute as CategoryAttribute).Category+"/";
				}
			}

			genericMenu.AddItem (new GUIContent (category + type.ToString().Split('.').Last()), false, new GenericMenu.MenuFunction2 (this.OnCreateAction),type);
		}
		genericMenu.ShowAsContext ();
	}

	private void OnCreateAction(object data){
		BaseAction action =(BaseAction) ScriptableObject.CreateInstance ((Type)data);
		action.name = action.GetType ().ToString ().Split('.').Last();
		AssetDatabase.AddObjectToAsset (action, state);
		AssetDatabase.SaveAssets();
		state.actions.Add(action);
	}

	private void RemoveAction(int index){
		DestroyImmediate(state.actions[index],true);
		state.actions.RemoveAt(index);
		AssetDatabase.SaveAssets();
	}

	private void DrawActionElement(int index){
		BaseAction action = state.actions [index];
		FieldInfo[] fields = action.GetType ().GetFields (BindingFlags.Public | BindingFlags.Instance);
		SerializedObject obj = new SerializedObject (action);
		obj.Update ();
		for (int cnt=0; cnt<fields.Length; cnt++) {
			if(action is AISystem.Actions.WaitForSeconds && fields [cnt].Name.Equals("queue")){
				obj.FindProperty (fields [cnt].Name).boolValue=true;
				GUI.enabled=false;
			}
			SerializedProperty mProperty=obj.FindProperty (fields [cnt].Name);
			if(mProperty != null){
				if(mProperty.isArray){
					bool foldOut = EditorPrefs.GetBool (mProperty.displayName, true);
		
					GUI.SetNextControlName (mProperty.displayName);
				
					string foldoutLabel=mProperty.displayName;
					foldOut =  EditorGUILayout.Foldout (foldOut, foldoutLabel);
					EditorPrefs.SetBool (mProperty.displayName, foldOut);	
//					Debug.Log ( mProperty.displayName + mProperty.GetHashCode () + " foldOut= " + foldOut);
					if (foldOut) {
						//画出size 和element
						GUILayout.BeginVertical();
						EditorGUI.indentLevel += 1;	//增加缩进
						mProperty.arraySize = int.Parse( EditorGUILayout.TextField("Size:", mProperty.arraySize.ToString()) );
						
						for (int i = 0; i < mProperty.arraySize; i++) {
							EditorGUILayout.PropertyField(mProperty.GetArrayElementAtIndex(i));
						}
						EditorGUI.indentLevel -= 1;//减少缩进
						GUILayout.EndVertical();
					}

//					EditorGUILayout.PropertyField (mProperty);


				}
				else{
					EditorGUILayout.PropertyField (mProperty);
				}
			}

		}
		GUI.enabled=true;
		obj.ApplyModifiedProperties ();
	}


	protected override void OnHeaderGUI ()
	{
		if (state != null) {
			GUILayout.BeginVertical ("IN BigTitle");
			EditorGUIUtility.labelWidth = 50;
			state.name = EditorGUILayout.TextField ("Name", state.name);
			GUILayout.EndVertical ();
		}
	}
	
	private void OnSceneView(SceneView sceneview)
	{
		if (!(state is AISystem.States.NavMeshAgent.Patrol)) {
			return;
		}

		Tools.current = Tool.None;
		HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));

		AISystem.States.NavMeshAgent.Patrol patrol= state as AISystem.States.NavMeshAgent.Patrol;
		
		if (patrol.path == null) {
			patrol.path= new Vector3[0];
		}
		List<Vector3> path = new List<Vector3> (patrol.path);
		
		
		Handles.color = Color.cyan;
		for (int index=0; index< path.Count; index++) {
			path [index] = Handles.PositionHandle (path [index], Quaternion.identity);
		}
		
		patrol.path = path.ToArray ();
		
		if(path.Count>=2){
			List<Vector3> curve;
			Handles.color = Color.red;
			if (AISystem.States.NavMeshAgent.Patrol.CatmullRom (path, out curve, 10, true)) {
				for (int n = 0; n < curve.Count - 1; n++) {
					Handles.DrawLine (curve [n], curve [n + 1]);
				}
			}
		}
		if (Event.current.type == EventType.MouseDown)	{
			if (Event.current.button == 0)				
			{
				Ray worldRay = HandleUtility.GUIPointToWorldRay (Event.current.mousePosition);
				RaycastHit hitInfo;			
				if (Physics.Raycast (worldRay, out hitInfo))
				{		
					List<Vector3> p= new List<Vector3>(patrol.path);
					p.Add(hitInfo.point);
					patrol.path=p.ToArray();
				}else{
					List<Vector3> p= new List<Vector3>(patrol.path);
					p.Add(worldRay.GetPoint(50));
					patrol.path=p.ToArray();
				}

				SceneView.RepaintAll();
				Event.current.Use ();

			}
		}
	}
}
