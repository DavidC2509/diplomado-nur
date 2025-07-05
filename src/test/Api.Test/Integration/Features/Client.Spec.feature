Feature: Probar las funcionalidades de cliente
 


Scenario: Listado cliente
    Given la siguiente solicitud
    """
    """
    When se solicita "sin" credenciales que se procese a la url "/api/client/list", usando el metodo "get"
    Then la respuesta debe tener el codigo de estado 200 
    And la respuesta "si" contener un listado vacio

Scenario: Obtener cliente por id
    Given la siguiente solicitud
    """
    """
    Given la siguiente entidad "client" registrada
    When se solicita "sin" credenciales que se procese a la url "/api/client/{RecordId}", usando el metodo "get"
    Then la respuesta debe tener el codigo de estado 200

Scenario: Guardar Direccion
    Given la siguiente solicitud
    """
    {
      "street": "Prueba",
      "city": "Test",
      "latituded": 1,
      "longitud": 2,
      "dateDelivery": "2025-07-05T08:12:25.194Z"
    }   
    """
    Given la siguiente entidad "client" registrada
    When se solicita "sin" credenciales que se procese a la url "/api/client/{RecordId}/address", usando el metodo "post"
    Then la respuesta debe tener el codigo de estado 200 
    And la respuesta debe contener un booleano