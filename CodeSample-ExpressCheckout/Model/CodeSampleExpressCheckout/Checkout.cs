namespace CodeSampleExpressCheckout {
	using System;
	using System.Web;
	using System.Collections.Generic;
	using PayPal;
	using PayPal.ExpressCheckout;
	using PayPal.Enum;
	using PayPal.Nvp;
	
	/// <summary>
	/// Exemplo simples de implementação da integração com a API Express Checkout do PayPal.
	/// </summary>
	public class Checkout {
		private Checkout () {}
		
		/// <summary>
		/// Cria uma instância de ExpressCheckoutApi que será utilizada na integração.
		/// </summary>
		/// <returns>
		/// A instância de ExpressCheckoutApi
		/// </returns>
		private static ExpressCheckoutApi expressCheckout() {
			string email = "informe-seu-email@exemplo.com";
			string password = "informe-sua-senha";
			string apiKey = "informe-sua-chave-da-API";
			
			//Descomente a linha abaixo após definir suas credenciais da API.
			throw new Exception("Você precisa definir suas credenciais da API antes de rodar o exemplo");
			
			return PayPalApiFactory.instance.ExpressCheckout(
			    email,
			    password,
			    apiKey
			);
		}
		
		/// <summary>
		/// Executa a operação no Sandbox ou em produção. Esse método existe para facilitar a modificação
		/// entre SandBox e produção, evitando ter que trocar todas as chamadas em pontos distintos do código.
		/// </summary>
		/// <param name='operation'>
		/// A operação que deverá ser executada.
		/// </param>
		private static void execute(ExpressCheckoutApi.Operation operation) {
			operation.sandbox().execute();
		}
		
		/// <summary>
		/// Configura a moeda e idioma da página de pagamento do PayPal.
		/// </summary>
		/// <param name='operation'>
		/// A operação que terá a moeda e idioma configurados.
		/// </param>
		private static void configureLocalization(ExpressCheckoutApi.Operation operation) {
			operation.CurrencyCode = CurrencyCode.BRAZILIAN_REAL;
			operation.LocaleCode = LocaleCode.BRAZILIAN_PORTUGUESE;
		}

		/// <summary>
		/// Configura o Express Checkout utilizando a operação SetExpressCheckout
		/// </summary>
		/// <param name='success'>
		/// Define a URL que o cliente será redirecionado pelo PayPal em caso de sucesso.
		/// </param>
		/// <param name='cancel'>
		/// Define a URL que o cliente será redirecionado pelo PayPal caso o ciente cancele.
		/// </param>
		public static string start(string success, string cancel) {
			//Cria a instância de SetExpressCheckoutOperation que usaremos para fazer a integração
			SetExpressCheckoutOperation SetExpressCheckout = expressCheckout().SetExpressCheckout(success, cancel);
			PaymentRequest paymentRequest = SetExpressCheckout.PaymentRequest(0);

			//Pega os itens do carrinho do cliente
			Dictionary<Product, int> items = Cart.Instance().Items;

			//Adiciona todos os itens do carrinho do cliente
			foreach (Product product in items.Keys) {
				int quantity = items[product];

				paymentRequest.addItem(product.Name, quantity, product.Price);
			}

			//Configura moeda e idioma
			configureLocalization(SetExpressCheckout);

			//Executa a operação
			execute(SetExpressCheckout);

			//Retorna a URL de redirecionamento
			return SetExpressCheckout.RedirectUrl;
		}
		
		/// <summary>
		/// Recupera os dados da transação usando a operação GetExpressCheckoutDetails e, então, utiliza a
		/// operação DoExpressCheckout para completar a transação.
		/// </summary>
		/// <param name='token'>
		/// O Token enviado pelo PayPal após o redirecionamento do cliente.
		/// </param>
		/// <param name='PayerID'>
		/// O id do cliente no PayPal, recebido após o redirecionamento do cliente.
		/// </param>
		/// <exception cref='Exception'>
		/// Se a transação falhar, uma exceção é disparada.
		/// </exception>
		public static ResponseNVP finalize(string token, string PayerID) {
			GetExpressCheckoutDetailsOperation GetExpressCheckout = expressCheckout().GetExpressCheckoutDetails(
				token
			);

			execute(GetExpressCheckout); //Executa a operação GetExpressCheckout

			//NVP da resposta do GetExpressCheckout
			ResponseNVP responseNVP = GetExpressCheckout.ResponseNVP;

			if (GetExpressCheckout.ResponseNVP.Ack == Ack.SUCCESS) {
				DoExpressCheckoutPaymentOperation DoExpressCheckout = expressCheckout().DoExpressCheckoutPayment(
					token, PayerID, PaymentAction.SALE
				);

				DoExpressCheckout.PaymentRequest(0).Amount = responseNVP.GetDouble("PAYMENTREQUEST_0_AMT");

				configureLocalization(DoExpressCheckout); //Configura moeda e idioma
				execute(DoExpressCheckout); //Executa a operação DoExpressCheckout

				if (DoExpressCheckout.ResponseNVP.Ack != Ack.SUCCESS) {
					throw new Exception();
				}
			}

			return responseNVP;
		}
	}
}