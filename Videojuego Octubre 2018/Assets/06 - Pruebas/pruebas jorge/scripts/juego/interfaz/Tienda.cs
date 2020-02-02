using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Purchasing;

public class Tienda : MonoBehaviour
{
    public Text txtMessage;
    string textFailureReason;
    List<PayoutDefinition> listPayouts = new List<PayoutDefinition> ();

    #region Dynamic Product - Se completan automaticamente con el Product ID
    public void Purchase_Consumable(Product _product)
    {
        print("Has recibido: " + _product.definition.payout.quantity + " " + _product.definition.payout.subtype);
        txtMessage.text = "Has recibido: " + _product.definition.payout.quantity + " " + _product.definition.payout.subtype;
        GameObject.Find("Datosguardados").GetComponent<DatosGuardados>().Monedas += (int)_product.definition.payout.quantity;
        GameObject.Find("Controlador").GetComponent<ControlBotonesMenu>().VerMonedas();
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
            txtMessage.text = "ERROR: " + textFailureReason + " / Producto: " + _product.definition.id;
        }
        else
        {
            print ("Cargando el producto...");
            txtMessage.text = "Cargando el producto...";
        }
    }

    #endregion

    #region Static Parameters - Les seteamos manualmente los parametros
    public void Custom_Consumable (int _product) 
    {
		print ("Has recibido: " + _product.ToString () + " monedas");
		txtMessage.text = "Has recibido: " + _product.ToString () + " monedas";
	}

    #endregion
}
