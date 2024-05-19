namespace MyVacation
{
    public class Database
    {
        private string link;
        private string airlines;
        private string daname;
        private string dtime;
        private string raname;
        private string rtime;
        private string _dtime;
        private string _daname;
        private string _rtime;
        private string _raname;

        public Database(string link, string airlines, string dtime, string daname, string rtime, string raname, string _dtime, string _daname, string _rtime, string _raname)
        {
            this.link = link;
            this.airlines = airlines;
            this.dtime = dtime;
            this.daname = daname;
            this.raname = raname;
            this.rtime = rtime;
            this._dtime = _dtime;
            this._daname = _daname;
            this._rtime = _rtime;
            this._raname = _raname;
        }

        public string Link { get { return link; } set { link = value; } }
        public string Airlines { get { return airlines; } set { airlines = value; } }
        public string daName { get { return daname; } set { daname = value; } }
        public string dTime { get { return dtime; } set { dtime = value; } }
        public string raName { get { return raname; } set { raname = value; } }
        public string rTime { get { return rtime; } set { rtime = value; } }
        public string _daName { get { return _daname; } set { _daname = value; } }
        public string _dTime { get { return _dtime; } set { _dtime = value; } }
        public string _raName { get { return _raname; } set { _raname = value; } }
        public string _rTime { get { return _rtime; } set { _rtime = value; } }
    }
}
