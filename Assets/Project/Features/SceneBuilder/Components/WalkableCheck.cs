using ME.ECS;
using ME.ECS.Collections;

namespace Project.Features.SceneBuilder.Components 
{
    public struct WalkableCheck : IStructCopyable<WalkableCheck>
    {
        public BufferArray<byte> Buffer;
        public BufferArray<byte> HeightBuffer;

        void IStructCopyable<WalkableCheck>.CopyFrom(in WalkableCheck other) 
        {
            ArrayUtils.Copy(other.Buffer, ref Buffer);
            ArrayUtils.Copy(other.HeightBuffer,ref HeightBuffer);
        }
        
        void IStructCopyable<WalkableCheck>.OnRecycle() 
        {
            PoolArray<byte>.Recycle(ref Buffer);
            PoolArray<byte>.Recycle(ref HeightBuffer);
        }
    }
}