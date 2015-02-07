using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Makezi.StateMachine {

	public class StateMachine<T> {

		protected T context;						// Reference to context which initialized the state machine

		public delegate void StateHandler();
		public event StateHandler onStateChanged;	// Event triggered when state changes

		private Dictionary<System.Type, State<T>> states = new Dictionary<System.Type, State<T>>();
		private Stack<State<T>> stateStack;			// Stack of states of type T

		/* Constructor */
		public StateMachine(T context, State<T> initialState){
			this.context = context;
			stateStack = new Stack<State<T>>();
			// Add initial state to the stack
			AddState(initialState);
			stateStack.Push(initialState);
			initialState.Enter();
		}

		/* Add states to dictionary of states possible in this machine */
		public void AddState(State<T> state){
			state.Init(this, context);
			states[state.GetType()] = state;
		}

		/* Set state of this machine which is of generic type T */ 
		public void SetState<R>() where R : State<T> {
			// Obtain object type and check if the passed state is a valid state within this machine
			var stateType = typeof(R);
			if(!states.ContainsKey(stateType)){
				Debug.LogError("State " + stateType + " does not exist");
				return;
			}

			var state = states[stateType];
			if(!stateStack.Contains(state)){
				// Push state to the top of the stack if non exists already
				stateStack.Push(state);
				state.Enter();
			}else{
				// Pop all states above desired state
				while(!CurrentState.Equals(state)){
					stateStack.Pop();
				}
				CurrentState.Enter();
			}

			// State change event triggered
			if(onStateChanged != null){
				onStateChanged();
			}
		}

		public void Update(){
			CurrentState.Update();
		}

		public void FixedUpdate(){
			CurrentState.FixedUpdate();
		}

		public State<T> CurrentState {
			get {
				return stateStack.Peek();
			}
		}

		/* Return true if current state has same object type as passed generic type R */
		public bool CurrentStateEquals<R>() where R : State<T> {
			return CurrentState.GetType() == typeof(R);
		}
	}
}