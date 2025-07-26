public interface IDialogueNodeVisitor
{
    public void Visit(BasicDialogueNode node);
    public void Visit(ChoiceDialogueNode node);
}
