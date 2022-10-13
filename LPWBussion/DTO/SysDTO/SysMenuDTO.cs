namespace LPWBussion.DTO.SysDTO
{
    public class SysMenuDTO
    {
        public int Id { get; set; }
        public string Icon { get; set; }
        public string MenuName { get; set; }
        public string UserName { get; set; }
        public string MenuUrl { get; set; }
        public int ParantId { get; set; }
        public SysMenuDTO Childe { get; set; }

    }
}
