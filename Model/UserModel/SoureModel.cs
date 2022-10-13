namespace Model.UserModel
{
    public class SoureModel
    {
        private DateTime? _createtime;
        private DateTime? _updatetime;
        [SqlSugar.SugarColumn(IsPrimaryKey = true)]
        public int Id { get; set; }
        public int IsDelete { get; set; }
        public DateTime CreateDateTime
        {
            get
            {
                return _createtime is null ? DateTime.Now : _createtime.Value;
            }
            set { _createtime = value; }
        }
        public DateTime? UpdateTime { get { return Id != 0 ? DateTime.Now : _updatetime; } set { _updatetime = value; } }
    }
}
