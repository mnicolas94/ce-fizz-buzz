using System.Collections;
using System.Threading.Tasks;

namespace Tests.PlayMode
{
    public class PlayModeTestsUtils
    {
        public static IEnumerator WaitForAsyncFunction(Task asyncTask)
        {
            bool finished = false;
            async void WaitAsync()
            {
                await asyncTask;
                finished = true;
            }
            WaitAsync();

            while (!finished)
            {
                yield return null;
            }
        }
    }
}