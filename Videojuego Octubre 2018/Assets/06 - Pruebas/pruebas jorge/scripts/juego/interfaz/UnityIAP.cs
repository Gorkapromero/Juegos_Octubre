using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Purchasing;



public class UnityIAP : MonoBehaviour {

	public Text txtMessage;
	string textFailureReason;
	bool isVip = false;
	bool skin_Legendary = false;
    List<PayoutDefinition> listPayouts = new List<PayoutDefinition> ();

    #region Dynamic Product - Se completan automaticamente con el Product ID

    public void Purchase_Consumable (Product _product) {
		print ("Has recibido: " + _product.definition.payout.quantity + " " + _product.definition.payout.subtype);
        txtMessage.text = "Has recibido: " + _product.definition.payout.quantity + " " + _product.definition.payout.subtype;
    }

	public void Purchase_NonConsumable (Product _product) {
		switch (_product.definition.id) {
			case "ID":
				skin_Legendary = true;
                break;
		}
		print ("Has desbloqueado: " + _product.definition.payout.data);
		txtMessage.text = "Has desbloqueado: " + _product.definition.payout.data;
	}

	public void Purchase_Subscription (Product _product) {
		isVip = true;
        listPayouts = (List<PayoutDefinition>)_product.definition.payouts; //Hace la conversion a Lista
        print ("Ahora eres " + _product.definition.payout.data + ". Conseguiste: " + listPayouts[1].quantity + " " + listPayouts[1].subtype + " y " + listPayouts[2].data);
        txtMessage.text = "Ahora eres " + _product.definition.payout.data + ". Conseguiste: " + listPayouts[1].quantity + " " + listPayouts[1].subtype + " y " + listPayouts[2].data;
    }


	public void Purchase_Failed (Product _product, PurchaseFailureReason _failureReason) {
		switch (_failureReason) {
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

		//Si el producto ya esta cargado, nos muestra el error; sino muestra el texto "Inicializando"
		if (_product != null) {
			print ("ERROR: " + textFailureReason + " / Producto: " + _product.definition.id);
			txtMessage.text = "ERROR: " + textFailureReason + " / Producto: " + _product.definition.id;
		} else {
			print ("Cargando el producto..");
			txtMessage.text = "Cargando el producto..";
		}
		
		
	}

	#endregion


	#region Static Parameters - Les seteamos manualmente los parametros

	public void Custom_Consumable (int _product) {
		print ("Has recibido: " + _product.ToString () + " monedas");
		txtMessage.text = "Has recibido: " + _product.ToString () + " monedas";
	}

	public void Custom_NonConsumable (string _product) {
		switch (_product) {
			case "NAME":
				skin_Legendary = true;
				break;
		}
		print ("Has desbloqueado: Skin " + _product);
		txtMessage.text = "Has desbloqueado: Skin " + _product;
	}

	public void Custom_Subscription (bool _product) {
		isVip = _product;
		print ("Tu estado de VIP ahora es: " + _product);
		txtMessage.text = "Tu estado de VIP ahora es: " + _product;
	}

	#endregion


}
