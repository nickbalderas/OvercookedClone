namespace Interfaces
{
    public interface IInteractable
    {
        public bool IsPlayerFacing { get; set; }

        public void Highlight(bool indicator);
    }
}