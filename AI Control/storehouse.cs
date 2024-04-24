using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class storehouse: Machine
{
    public storehouse()
    {
        type = 0;
        placePosition = new Vector3(0,0.5f, 0);
    }
    public override void Start()
    {
        base.Start();
        model = GameObject.Instantiate(ProductPrefab);
        model.transform.SetParent(this.transform);
        model.transform.localPosition = placePosition;
        model.GetComponent<Rigidbody>().isKinematic = true;
    }
    public override void pick(Worker worker)
    {
        Debug.Log("Starting pick at storehouse: " + id);
        GameObject productGo = GameObject.Instantiate(model);
        var product = productGo.GetComponent<Product>();
        product.id = idCount++;       
        worker.holdingObject = productGo;
        productGo.transform.SetParent(worker.transform);
        productGo.transform.localPosition =new Vector3(0,1,1);
        productGo.transform.localScale = new Vector3(1,1,1);
        productGo.transform.localRotation = Quaternion.Euler(90,0,0);
        product.ChangeState(Product.state.Pick);
    }
    public override void place(GameObject target)
    {
        Debug.Log("Starting place at storehouse: " + id);
        Destroy(model);
        target.transform.SetParent(this.transform);
        target.transform.localPosition = placePosition;
        model = target;
        model.GetComponent<Product>().ChangeState(Product.state.Place);
    }
    public override void operate()
    {
        Debug.Log("Starting operate at storehouse: " + id);
    }
}