namespace CustomTypes
{
    public class MVCTemporaryObject<M, V, C>
    {
        public M Model { get; set; } = default;
        public V View { get; set; } = default;
        public C Controller { get; set; } = default;
    }
}