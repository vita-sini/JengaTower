using UnityEngine;

namespace Gameplay
{
    public class Release
    {
        public void FreeBlock(Rigidbody selectedBlock)
        {
            selectedBlock.constraints = RigidbodyConstraints.None;
            selectedBlock = null;
        }
    }
}
