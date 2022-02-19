namespace UniCorn.Core
{
    public abstract class AbstractSystem<LAYOUT_TYPE>
    {
        protected LAYOUT_TYPE _layout;

        public void SetLayout(LAYOUT_TYPE layout)
        {
            _layout = layout;
            OnLayoutInitialized();
        }

        protected abstract void OnLayoutInitialized();
    }
}
