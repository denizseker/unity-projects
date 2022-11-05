using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullBand : MonoBehaviour
{
    //Objelerin eski pozisyonlar�
    private Vector3 manOldPosition;
    public Vector3 manSpawnPoint;
    private Vector3 slingOldPosition;
    //Player objesi , collider� ve rigidbodysi
    public GameObject manObj;
    private Rigidbody manRb;
    private BoxCollider manCollider;

    private bool canShoot = true;

    public LineRenderer lineVisual;
    public int lineSegment = 10;


    private float mZCoord;
    public float force;


    //Proje a��l���nda ilk atamalar.
    public void Start()
    {
        lineVisual.positionCount = lineSegment;

        manRb = manObj.GetComponent<Rigidbody>();

        manCollider = manObj.GetComponent<BoxCollider>();

    }

    //At�� g�stergesi �izme fonksiyonu
    void Visualize(Vector3 vo)
    {
        for (int i = 0; i < lineSegment; i++)
        {
            Vector3 pos = CalculatePosInTime(vo, i / (float)(lineSegment));
            lineVisual.SetPosition(i, pos);
        }
    }


    //At�� g�stergesinin velocity de�eri ile zamana g�re hesaplanmas�
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
        //Mouse t�klanmas� ger�ekle�tikten sonra player objesinin ve sapan�n eski konumlar� depolan�yor.
        manOldPosition = manObj.transform.position;
        slingOldPosition = transform.position;
        //Mouse ve objenin 3d d�nyadaki mesafesi sonradan kullan�lmak �zere depolan�yor.
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
    }


    //Player objesinin ate�lenmesi
    private void Shoot()
    {
        //Objenin �ekildikten sonraki pozisyonu �ekilmeden �nceki pozisyonundan ��kart�l�p �nceden belirlenen force de�eri ile �arp�l�yor. Ard�ndan �ekildi�i y�n�n tersine gitmesi i�in -1 de�eri ile �arp�l�yor.
        Vector3 manForce = (manObj.transform.position - manOldPosition) * force * -1;
        //Velocity de�eri hesapland�ktan sonra player objesine g�� uygulan�yor.
        manRb.velocity = manForce;
        
    }

    //Mouseun 3d d�nya �zerindeki konumu hesaplan�yor ve fonksiyon ile geri d�nd�r�l�yor.
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    IEnumerator DestroyAndCreateCoroutine() //Player g�nderildikten sonra destroy edilip yenisi ekleniyor. 
    {
        if (!canShoot)
        {
            yield return new WaitForSeconds(4);
            //Eski player objesi imha ediliyor.
            Destroy(manObj);
            //Yeni obje olu�turuluyor.
            manObj = Instantiate(manObj, manSpawnPoint, Quaternion.identity);
            //Ate�lemeden sonra yeni objenin bulunabilmesi i�in isim de�i�ikli�i (Aksi halde obje Man(Clone) olarak adland�r�l�yor.)
            manObj.name = "Man";
            //Yeni obje bant objesinin childi olarak atan�yor.
            manObj.transform.SetParent(gameObject.transform);
            //Yeni olu�turulan player objesi i�in componentler �ekiliyor.
            manRb = manObj.GetComponent<Rigidbody>();
            manCollider = manObj.GetComponent<BoxCollider>();
            //Yeni player objesinin gravitiy ve collider de�erleri kapat�l�yor. Aksi halde sapandan d���yor veya sapan�n collideri ile etkile�ime giriyor.
            manRb.useGravity = false;
            manCollider.enabled = false;
            canShoot = true;
        }
        
    }


    private void OnMouseUp()
    {
        if (canShoot)
        {
            //Ate�
            Shoot();
            canShoot = false;
            //Player objesi sapandan ��kt�ktan sonra parent objeden ayr�l�yor.
            Transform childToRemove = gameObject.transform.Find("Man");
            childToRemove.parent = null;
            //Sapan eski konumuna d�n�yor.
            transform.position = slingOldPosition;
            //Player objesi sapandan ayr�ld�ktan sonra gravity ve collider de�erleri a��l�yor.
            manRb.useGravity = true;
            manCollider.enabled = true;
            //At�� i�lemi tamamland��� i�in at�� hizas� �izimi kapat�l�yor.
            lineVisual.enabled = false;
            //Yeni player objesi olu�turmak ve eskisini yoketmek i�in coroutine ba�lat�l�yor.
            StartCoroutine(DestroyAndCreateCoroutine());
        }

    }


    private void OnMouseDrag()
    {
        if (canShoot)
        {
            //At�� g�stergesi a��l�yor.
            lineVisual.enabled = true;
            //At�� g�stergesi fonksiyonuna player objesinin velocity de�eri g�nderiliyor.
            Vector3 manForce = (manObj.transform.position - manOldPosition) * force * -1;
            //At�� g�stergesi �iziliyor.
            Visualize(manForce);
            //Mouse sol click ile bas�l� tutuldu�unda sapan�n konumu, mouse konumuna g�re ayarlan�yor.
            transform.position = new Vector3(transform.position.x, transform.position.y, GetMouseWorldPos().z);
        }
        
    }


}

 
