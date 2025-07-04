Feature: Probar las funcionalidades de cliente
 


Scenario: Listado cliente
    Given la siguiente solicitud
    """
    """
    When se solicita "sin" credenciales que se procese a la url "/api/client/list", usando el metodo "get"
    Then la respuesta debe tener el codigo de estado 200 
    And la respuesta "si" contener un listado vacio