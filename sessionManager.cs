using System;
using System.Collections;

namespace CSharpAPI6
{
	public class SessionTokenManager
	{
        private static TimestampedSession m_sessionToken;

		private class TimestampedSession
		{
			public string SessionToken;
			public DateTime Timestamp;

			public TimestampedSession(string session, DateTime timestamp)
			{
				SessionToken = session;
				Timestamp = timestamp;
			}
		}

		static SessionTokenManager()
		{
            m_sessionToken = new TimestampedSession(null, new DateTime());
		}

		public static string GetSessionToken()
		{
			string token = null;
			lock (m_sessionToken)
			{
				token = m_sessionToken.SessionToken;
			}

			return token;
		}

        public static bool HasSessions
        {
            get
            {
                TimeSpan tokenAge = DateTime.Now - m_sessionToken.Timestamp;
                if (m_sessionToken.SessionToken != null && tokenAge.TotalMinutes < 60)
                    return true;
                else
                    return false;
            }
        }

		public static void ReturnSessionToken(string token)
		{
			lock (m_sessionToken)
			{
				m_sessionToken.SessionToken = token;
                m_sessionToken.Timestamp = DateTime.Now;
			}
		}
	}
}
