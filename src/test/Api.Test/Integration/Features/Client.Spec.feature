Feature: Probar las funcionalidades de cliente
 

Scenario: Registrar Cliente
    Given la siguiente solicitud
    """
    {
    "Name": "Prueba Entrada",
    "Phone": "75324397",
    "Email": "davidfernando.chavez777@gmail.com"
    }
    """
    When se solicita "sin" credenciales que se procese a la url "/api/client", usando el metodo "post"
    Then la respuesta debe tener el codigo de estado 200 
    And la respuesta debe contener un booleano