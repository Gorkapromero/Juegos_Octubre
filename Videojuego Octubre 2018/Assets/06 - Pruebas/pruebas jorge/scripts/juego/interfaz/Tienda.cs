using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Purchasing;
using UnityEngine.EventSystems;

public class Tienda : MonoBehaviour
{
    public GameObject clickScreen;
    //public Text txtMessage;
    string textFailureReason;
    List<PayoutDefinition> listPayouts = new List<PayoutDefinition> ();

    public GameObject destello1;
    public GameObject destello2;
    public GameObject destello3;

    bool cofre1;
    bool cofre2;
    bool cofre3;

    public GameObject particulasCofre;
    public Transform PosicionParticulas;
    public GameObject GrupoTexto;
    public Sprite[] ImagenMonedasConseguidas;
    int NCofre;
    public GameObject parent;
    //public Text Texto;

    Product Producto;

    #region Dynamic Product - Se completan automaticamente con el Product ID
    public void Purchase_Consumable(Product _product)
    {
        Producto = _product;
        print("BOTON PULSADO: "+_product.definition.id);
        //iniciar animaciones cofres
        switch(_product.definition.id)
        {
            case "500.coins":
                clickScreen.SetActive(true);
                GameObject.Find("gpr_cofre_01").transform.parent = parent.transform;
                GameObject.Find("cofre_01").GetComponent<Animator>().Play("animation_cofre_1");
                destello1.SetActive(true);
                NCofre = 0;
            break;

            case "1500.coins":
                clickScreen.SetActive(true);
                GameObject.Find("gpr_cofre_02").transform.parent = parent.transform;
                GameObject.Find("cofre_02").GetComponent<Animator>().Play("animation_cofre_2");
                destello2.SetActive(true);
                NCofre = 1;
            break;

            case "5000.coins":
                clickScreen.SetActive(true);
                GameObject.Find("gpr_cofre_03").transform.parent = parent.transform;
                GameObject.Find("cofre_03").GetComponent<Animator>().Play("animation_cofre_3");
                destello3.SetActive(true);
                NCofre = 2;
            break;
        }
        print("Has recibido: " + _product.definition.payout.quantity + " " + _product.definition.payout.subtype);
        //txtMessage.text = "Has recibido: " + _product.definition.payout.quantity + " " + _product.definition.payout.subtype;
        GameObject.Find("Datosguardados").GetComponent<DatosGuardados>().Monedas += (int)_product.definition.payout.quantity;
        GameObject.Find("Controlador").GetComponent<ControlBotonesMenu>().VerMonedas();
        GameObject.Find("Datosguardados").GetComponent<DatosGuardados>().Save();
    }

    public void Purchase_Failed ( Product _product,PurchaseFailureReason _failureReason)
    {
        switch(_failureReason)
        {
            case PurchaseFailureReason.PurchasingUnavailable:
            textFailureReason = "Compra no disponible";
            break;
            case PurchaseFailureReason.ExistingPurchasePending:
				textFailureReason = "Existencia de compra pendiente";
				break;
			case PurchaseFailureReason.ProductUnavailable:
				textFailureReason = "Producto no disponible";
				break;
			case PurchaseFailureReason.SignatureInvalid:
				textFailureReason = "Firma invalida";
				break;
			case PurchaseFailureReason.UserCancelled:
				textFailureReason = "Usuario cancelado";
				break;
			case PurchaseFailureReason.PaymentDeclined:
				textFailureReason = "Pago rechazado";
				break;
			case PurchaseFailureReason.DuplicateTransaction:
				textFailureReason = "Transaccion duplicada";
				break;
			case PurchaseFailureReason.Unknown:
				textFailureReason = "Desconocido";
				break;
        }

        //Si el producto ya esta cargado, nos muestra el error, si no muestra el texto "Inicializado"
        if(_product != null)
        {
            print("ERROR: " + textFailureReason + " / Producto: " + _product.definition.id);
            //txtMessage.text = "ERROR: " + textFailureReason + " / Producto: " + _product.definition.id;
        }
        else
        {
            print ("Cargando el producto...");
            //txtMessage.text = "Cargando el producto...";
        }
    }

    #endregion

    #region Static Parameters - Les seteamos manualmente los parametros
    public void Custom_Consumable (int _product) 
    {
		print ("Has recibido: " + _product.ToString () + " monedas");
		//txtMessage.text = "Has recibido: " + _product.ToString () + " monedas";
	}

    #endregion

    public void ClickOnScreen()
    {
        switch(Producto.definition.id)
        {
            case "500.coins":
                GameObject.Find("cofre_01").GetComponent<Animator>().Play("animation_cofre_preapertura_01");
                //Invoke("IniciarParticulas",1f);
                IniciarParticulas();
            break;

            case "1500.coins":
                GameObject.Find("cofre_02").GetComponent<Animator>().Play("animation_cofre_preapertura_02");
                //Invoke("IniciarParticulas",1f);
                IniciarParticulas();
            break;

            case "5000.coins":
                GameObject.Find("cofre_03").GetComponent<Animator>().Play("animation_cofre_preapertura_03");
                //Invoke("IniciarParticulas",1f);
                IniciarParticulas();
            break;
        }
    }

    public void IniciarParticulas()
    {
        Instantiate(particulasCofre,PosicionParticulas);
        Invoke("textoFinal",2f);
    }

    void textoFinal()
    {
        GrupoTexto.SetActive(true);
        GrupoTexto.GetComponent<Image>().sprite = ImagenMonedasConseguidas[NCofre];
        //Texto.text = "Has recibido: " + Producto.definition.payout.quantity + " " + Producto.definition.payout.subtype;
    }

    public void ClickTexto()
    {
        GrupoTexto.SetActive(false);
        switch(Producto.definition.id)
        {
            case "500.coins":
                GameObject.Find("cofre_01").GetComponent<Animator>().Play("New State");
                GameObject.Find("gpr_cofre_01").transform.parent = GameObject.Find("Paquete1").transform;
                //Invoke("IniciarParticulas",1f);
                destello1.SetActive(false);
            break;

            case "1500.coins":
                GameObject.Find("cofre_02").GetComponent<Animator>().Play("New State");
                GameObject.Find("gpr_cofre_02").transform.parent = GameObject.Find("Paquete2").transform;
                //Invoke("IniciarParticulas",1f);
                destello2.SetActive(false);
            break;

            case "5000.coins":
                GameObject.Find("cofre_03").GetComponent<Animator>().Play("New State");
                GameObject.Find("gpr_cofre_03").transform.parent = GameObject.Find("Paquete3").transform;
                //Invoke("IniciarParticulas",1f);
                destello3.SetActive(false);
            break;
        }
        clickScreen.SetActive(false);

    }
}
