using System.Collections.Generic;
using UnityEngine;
using UGameCore.Utilities;
using System.Linq;

namespace SanAndreasUnity.UI
{

	public class StatsWindow : PauseMenuWindow
    {
		int m_tabIndex = 0;
        Vector2 m_scrollViewPos = Vector2.zero;
        readonly Utilities.Stats.GetStatsContext m_statsContext = new Utilities.Stats.GetStatsContext(true);


		StatsWindow()
        {
			// set default parameters

			this.windowName = "Stats";
			this.useScrollView = false;

		}

		void Start ()
        {
			this.RegisterButtonInPauseMenu ();

			// adjust rect
			this.windowRect = Utilities.GUIUtils.GetCenteredRectPerc(new Vector2(0.8f, 0.8f));
		}


		protected override void OnWindowGUI ()
		{
            Utilities.Stats.DisplayRect = this.windowRect;
            var categories = Utilities.Stats.Categories.ToArray();
            m_tabIndex = GUIUtils.TabsControl(m_tabIndex, categories);
            if (m_tabIndex >= 0 && categories.Length > 0)
            {
                m_scrollViewPos = GUILayout.BeginScrollView(m_scrollViewPos);
                var stats = Utilities.Stats.Entries.ElementAt(m_tabIndex).Value;
                foreach (var stat in stats)
                {
                    if (!string.IsNullOrEmpty(stat.text))
                        GUILayout.Label(stat.text);

                    m_statsContext.stringBuilder.Clear();
                    stat.getStatsAction?.Invoke(m_statsContext);
                    if (m_statsContext.stringBuilder.Length > 0)
                        GUILayout.Label(m_statsContext.stringBuilder.ToString());
                }
                GUILayout.EndScrollView();
            }
		}

	}

}
