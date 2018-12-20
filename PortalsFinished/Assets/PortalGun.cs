using UnityEngine;

public class PortalGun : MonoBehaviour
{
    public Portal Red;
    public Portal Blue;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.layer != 10) return;
                
                if (Input.GetMouseButtonDown(0))
                {
                    Red.transform.rotation = Quaternion.LookRotation(hit.normal);
                    Red.transform.position = hit.point + Red.transform.forward * 0.6f;
                }
                else
                {
                    Blue.transform.rotation = Quaternion.LookRotation(hit.normal);
                    Blue.transform.position = hit.point + Blue.transform.forward * 0.6f;
                }
            }
        }
    }
}
