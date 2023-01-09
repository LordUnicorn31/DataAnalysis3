using UnityEngine;

namespace Gamekit3D.GameCommands
{

    public class GameplayCounter : GameCommandHandler
    {
        public int currentCount = 0;
        public int targetCount = 3;
        private int enemyCount = 0; 

        [Space]
        [Tooltip("Send a command when increment is performed. (optional)")]
        public SendGameCommand onIncrementSendCommand;
        [Tooltip("Perform an action when increment is performed. (optional)")]
        public GameCommandHandler onIncrementPerformAction;
        [Space]
        [Tooltip("Send a command when target count is reacted. (optional)")]
        public SendGameCommand onTargetReachedSendCommand;
        [Tooltip("Perform an action when target count is reacted. (optional)")]
        public GameCommandHandler onTargetReachedPerformAction;


        public override void PerformInteraction()
        {
            enemyCount++;

            currentCount += 1;
            if (currentCount >= targetCount)
            {
                if (onTargetReachedPerformAction != null) onTargetReachedPerformAction.PerformInteraction();
                if (onTargetReachedSendCommand != null) onTargetReachedSendCommand.Send();
                isTriggered = true;
            }
            else
            {
                if (onIncrementPerformAction != null) onIncrementPerformAction.PerformInteraction();
                if (onIncrementSendCommand != null) onIncrementSendCommand.Send();
                isTriggered = false;
            }

            SendData(ProcessData());
        }

        public WWWForm ProcessData()
        {
            WWWForm form = new WWWForm();
            form.AddField("enemyCount", enemyCount);
            return form;
        }

        public void SendData(WWWForm form)
        {
            WWW www = new WWW("https://citmalumnes.upc.es/~jordiea3/Kills.php");
            

            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.Log(www.error);
            }
        }

    }

}
