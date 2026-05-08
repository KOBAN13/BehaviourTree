using Game.AI.BehaviourTree.Actions;
using Game.AI.BehaviourTree.Beliefs;
using Game.AI.BehaviourTree.Nodes.Action;

namespace Game.Builder
{
    public class ActionBuilder
    {
        private AgentAction _agentAction;

        public ActionBuilder(string name)
        {
            _agentAction = new AgentAction(name);
        }
        
        public ActionBuilder WithActionStrategy(IActionStrategy strategy)
        {
            _agentAction.SetActionStrategy(strategy);
            return this;
        }

        public ActionBuilder WithPrecondition(AgentBelief precondition)
        {
            _agentAction.Precondition.Add(precondition);
            return this;
        }

        public AgentAction BuildAgentAction() => _agentAction;

    }
}