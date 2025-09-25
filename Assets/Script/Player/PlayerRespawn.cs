using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField]private AudioClip checkpointSound; //Sound that we'll play when picking up a new checkpoint
    private Transform currentCheckpoint; // we'll store ur last checkpoint here
    private Health playerHealth;
    private UIManager uiManager;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uiManager = FindAnyObjectByType<UIManager>();
    }
    public void RespawnPlayer()
    {
        //Check if check point availablity
        if(currentCheckpoint == null)
        {
            //Show game over screen
            uiManager.GameOver();
            return; //Don't execute the rest of this function
        }
        transform.position = currentCheckpoint.position; // Move player to checkpoint position
        playerHealth.Respawn();//Restore player health and reset animation

        //Move camera to checkpoint room (for thi to work the checkpoint onjects has to placed as a child of the room object)
        Camera.main.GetComponent<CameraControl>().MoveToNewRoom(currentCheckpoint.parent);
    }

    //Activate checkpoints
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform; // Store the checkpoint that we activated as the current one
            SoundManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false; //Deactivate checkpoint collider
            collision.GetComponent<Animator>().SetTrigger("Appear"); ; //Trigger checkpoint animation
        }
    }
}
