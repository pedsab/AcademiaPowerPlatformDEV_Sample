function HelloWorld(executionContext) {

	var formContext = executionContext.getFormContext();

	//console.log(executionContext);

	//var name = formContext.getAttribute('name').getValue();

	//console.log(name);

	//formContext.getAttribute('accountnumber').setValue(name + "_12345678");


	//alert('TESTANDO FIDDLER');

	//var tickersymbol = formContext.getAttribute('tickersymbol').getValue();

	//if (tickersymbol === 'TEL') {

	//	formContext.getControl('accountnumber').setDisabled(true);

	//	formContext.getControl('telephone1').setVisible(true);
	//	formContext.getControl('fax').setVisible(false);
	//	formContext.getControl('websiteurl').setVisible(false);
	//}
	//else if (tickersymbol === 'FAX') {

	//	formContext.getControl('telephone1').setVisible(false);
	//	formContext.getControl('fax').setVisible(true);
	//	formContext.getControl('websiteurl').setVisible(false);
	//}
	//else {

	//	formContext.getControl('telephone1').setVisible(false);
	//	formContext.getControl('fax').setVisible(false);
	//	formContext.getControl('websiteurl').setVisible(true);
	//}

	//Xrm.WebApi.retrieveMultipleRecords("account", "?$select=name&$top=3").then(

	//	function success(result) {
	//		for (var i = 0; i < result.entities.length; i++) {
	//			console.log(result.entities[i]);
	//		}
	//		// perform additional operations on retrieved records
	//	},

	//	function (error) {
	//		console.log(error.message);
	//		// handle error conditions
	//	}
	//);

	//var data =
	//{
	//	"name": "Sample Account",
	//	"creditonhold": false,
	//	"address1_latitude": 47.639583,
	//	"description": "This is the description of the sample account",
	//	"revenue": 5000000,
	//	"accountcategorycode": 1
	//}

	//// create account record
	//Xrm.WebApi.createRecord("account", data).then(

	//	function success(result) {
	//		console.log("Account created with ID: " + result.id);
	//		// perform operations on record creation
	//	},
	//	function (error) {
	//		console.log(error.message);
	//		// handle error conditions
	//	}
	//);
	//var formOptions = {
	//	"entityName": "contact"
	//};

	//Xrm.Navigation.openForm(formOptions);

	formContext.ui.setFormNotification("sessão de treinamento de power platform...", "ERROR", "1");
	formContext.ui.setFormNotification("sessão de treinamento de power platform...", "WARNING", "2");
	formContext.ui.setFormNotification("sessão de treinamento de power platform...", "INFO", "3");
}


function OpenCurrentSystemUser(executionContext) {

	debugger;

	console.log(executionContext);

	Xrm.Navigation.openAlertDialog({
		"text": "Abrindo o registro do usuário logado." 
	})

	var userSettings = Xrm.Utility.getGlobalContext().userSettings;
	var userId = userSettings.userId;

	console.log(userId);

	Xrm.Navigation.openForm({

		"entityName": "systemuser",
		"entityId": userId
	});
}