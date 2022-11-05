using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullBand : MonoBehaviour
{
    //Objelerin eski pozisyonlarý
    private Vector3 manOldPosition;
    public Vector3 manSpawnPoint;
    private Vector3 slingOldPosition;
    //Player objesi , colliderý ve rigidbodysi
    public GameObject manObj;
    private Rigidbody manRb;
    private BoxCollider manCollider;

    private bool canShoot = true;

    public LineRenderer lineVisual;
    public int lineSegment = 10;


    private float mZCoord;
    public float force;


    //Proje açýlýþýnda ilk atamalar.
    public void Start()
    {
        lineVisual.positionCount = lineSegment;

        manRb = manObj.GetComponent<Rigidbody>();

        manCollider = manObj.GetComponent<BoxCollider>();

    }

    //Atýþ göstergesi çizme fonksiyonu
    void Visualize(Vector3 vo)
    {
        for (int i = 0; i < lineSegment; i++)
        {
            Vector3 pos = CalculatePosInTime(vo, i / (float)(lineSegment));
            lineVisual.SetPosition(i, pos);
        }
    }


    //Atýþ göstergesinin velocity deðeri ile zamana göre hesaplanmasý
    Vector3 CalculatePosInTime(Vector3 vo,float time)
    {
        Vector3 Vxz = vo;
        Vxz.y = 0f;

        Vector3 result = manOldPosition + vo * time;

        float sY = (-0.5f * Mathf.Abs(Physics.gravity.y) * (time * time)) + (vo.y * time) + manOldPosition.y;

        result.y = sY;

        return result;
    }


    private void OnMouseDown()
    {
        //Mouse týklanmasý gerçekleþtikten sonra player objesinin ve sapanýn eski konumlarý depolanýyor.
        manOldPosition = manObj.transform.position;
        slingOldPosition = transform.position;
        //Mouse ve objenin 3d dünyadaki mesafesi sonradan kullanýlmak üzere depolanýyor.
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
    }


    //Player objesinin ateþlenmesi
    private void Shoot()
    {
        //Objenin çekildikten sonraki pozisyonu çekilmeden önceki pozisyonundan çýkartýlýp önceden belirlenen force deðeri ile çarpýlýyor. Ardýndan çekildiði yönün tersine gitmesi için -1 deðeri ile çarpýlýyor.
        Vector3 manForce = (manObj.transform.position - manOldPosition) * force * -1;
        //Velocity deðeri hesaplandýktan sonra player objesine güç uygulanýyor.
        manRb.velocity = manForce;
        
    }

    //Mouseun 3d dünya üzerindeki konumu hesaplanýyor ve fonksiyon ile geri döndürülüyor.
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    IEnumerator DestroyAndCreateCoroutine() //Player gönderildikten sonra destroy edilip yenisi ekleniyor. 
    {
        if (!canShoot)
        {
            yield return new WaitForSeconds(4);
            //Eski player objesi imha ediliyor.
            Destroy(manObj);
            //Yeni obje oluþturuluyor.
            manObj = Instantiate(manObj, manSpawnPoint, Quaternion.identity);
            //Ateþlemeden sonra yeni objenin bulunabilmesi için isim deðiþikliði (Aksi halde obje Man(Clone) olarak adlandýrýlýyor.)
            manObj.name = "Man";
            //Yeni obje bant objesinin childi olarak atanýyor.
            manObj.transform.SetParent(gameObject.transform);
            //Yeni oluþturulan player objesi için componentler çekiliyor.
            manRb = manObj.GetComponent<Rigidbody>();
            manCollider = manObj.GetComponent<BoxCollider>();
            //Yeni player objesinin gravitiy ve collider deðerleri kapatýlýyor. Aksi halde sapandan düþüyor veya sapanýn collideri ile etkileþime giriyor.
            manRb.useGravity = false;
            manCollider.enabled = false;
            canShoot = true;
        }
        
    }


    private void OnMouseUp()
    {
        if (canShoot)
        {
            //Ateþ
            Shoot();
            canShoot = false;
            //Player objesi sapandan çýktýktan sonra parent objeden ayrýlýyor.
            Transform childToRemove = gameObject.transform.Find("Man");
            childToRemove.parent = null;
            //Sapan eski konumuna dönüyor.
            transform.position = slingOldPosition;
            //Player objesi sapandan ayrýldýktan sonra gravity ve collider deðerleri açýlýyor.
            manRb.useGravity = true;
            manCollider.enabled = true;
            //Atýþ iþlemi tamamlandýðý için atýþ hizasý çizimi kapatýlýyor.
            lineVisual.enabled = false;
            //Yeni player objesi oluþturmak ve eskisini yoketmek için coroutine baþlatýlýyor.
            StartCoroutine(DestroyAndCreateCoroutine());
        }

    }


    private void OnMouseDrag()
    {
        if (canShoot)
        {
            //Atýþ göstergesi açýlýyor.
            lineVisual.enabled = true;
            //Atýþ göstergesi fonksiyonuna player objesinin velocity deðeri gönderiliyor.
            Vector3 manForce = (manObj.transform.position - manOldPosition) * force * -1;
            //Atýþ göstergesi çiziliyor.
            Visualize(manForce);
            //Mouse sol click ile basýlý tutulduðunda sapanýn konumu, mouse konumuna göre ayarlanýyor.
            transform.position = new Vector3(transform.position.x, transform.position.y, GetMouseWorldPos().z);
        }
        
    }


}

 
