using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.Collections;

public class MSPPostProcess  {

	private const string BUNLDE_KEY = "SA_PP_BUNLDE_KEY";

	[PostProcessBuild(48)]
	public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject) {

		#if UNITY_IPHONE
		string Accounts = "Accounts.framework";
		if(!ISDSettings.Instance.frameworks.Contains(Accounts)) {
			ISDSettings.Instance.frameworks.Add(Accounts);
		}


		string SocialF = "Social.framework";
		if(!ISDSettings.Instance.frameworks.Contains(SocialF)) {
			ISDSettings.Instance.frameworks.Add(SocialF);
		}

		string MessageUI = "MessageUI.framework";
		if(!ISDSettings.Instance.frameworks.Contains(MessageUI)) {
			ISDSettings.Instance.frameworks.Add(MessageUI);
		}

		#endif
	}

}
