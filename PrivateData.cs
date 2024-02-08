namespace Storm;

using Shaders;

public abstract partial class Game
{
    private class ObjectList : List<GameObject> {
        public enum SortType {
            update,
            render,
        }
        private SortType st;
        public ObjectList(SortType st)
        {
            this.st = st;
        }
        public new void Add(GameObject element) {
            base.Add(element);
            if (st == SortType.update)
            {
                Sort(new UpdateIComparer());
            }
            if (st == SortType.render)
            {
                Sort(new RenderIComparer());
            }
        }

        private class UpdateIComparer : IComparer<GameObject> {
            public int Compare(GameObject? x, GameObject? y)
            {
                int sort = x!.updateIndex.CompareTo(y!.updateIndex);
                return sort;
            }
        }
        private class RenderIComparer : IComparer<GameObject> {
            public int Compare(GameObject? x, GameObject? y)
            {
                int sort = x!.renderIndex.CompareTo(y!.renderIndex);
                return sort;
            }
        }
    }

    private class Canvas : Form
    {
        public Canvas()
        {
            this.AutoScaleMode = AutoScaleMode.Font;
            this.DoubleBuffered = true;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

    }
}