namespace HandmadeShop.Application.Patterns.Singleton
{
    public class UserConnectionManager
    {
        private static readonly Dictionary<string, List<string>> _userConnection = new Dictionary<string, List<string>>();
        private static readonly object _lock = new object();

        public void KeepUserConnection(string userId, string connectionId)
        {
            lock (_lock)
            {
                if (!_userConnection.ContainsKey(userId))
                {
                    _userConnection[userId] = new List<string>();
                }
                if (!_userConnection[userId].Contains(connectionId))
                {
                    _userConnection[userId].Add(connectionId);
                }
            }
        }

        public void RemoveUserConnection(string connectionId)
        {
            lock (_lock)
            {
                foreach (var userId in _userConnection.Keys)
                {
                    if (_userConnection[userId].Contains(connectionId))
                    {
                        _userConnection[userId].Remove(connectionId);
                        if (_userConnection[userId].Count == 0)
                        {
                            _userConnection.Remove(userId);
                        }
                        break;
                    }
                }
            }
        }

        public List<string> GetInstance(string userId)
        {
            lock (_lock)
            {
                return _userConnection.ContainsKey(userId)
                        ? _userConnection[userId] : new List<string>();
            }
        }
    }
}