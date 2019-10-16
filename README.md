# GatewayGraph
Ejemplo de API Gateway creado con GraphQL utilizando HotChocolate (libreria de C# para creación de server GraphQL)

1°: Para desplegarlo como Imagen docker, cree un recurso de Azure Container Registry (ACR) y luego clone el repositorio en los archivos de la nuve (utiliza el Cloud Shell de Azure para hacerlo y selecciona Bash).

2°: Una vez clonada la API, ejecute el comando con la cual creará la imagen.

az acr build --image [nombre de la imagen] --registry [nombre del Registro de Contenedor en donde guardará la imagen] [~/La dirección del dockerfile en el Cloud Shell que ha recibido el repositorio clonado]

Al tener la imagen lista, puedes verificar creandole una instancia de imagen para ver si funciona o no.

3°: De ahi, debes crearte un recurso de Kubernetes para guardar los apis como microservicios ahora que los tienes como imagen. Tras crearlo, expon los credenciales para que el ACR lo pueda utilizar usando el comando:

az aks get-credentials -n [nombre del cluster] -g [Grupo de recursos a la que pertenece]

4°: De ahi le vamos a crear un secreto con la cual vamos a entablar comunicación entre el ACR y el Kubernetes, utilizando este comando para crear un secreto:

kubectl create secret docker-registry [nombre del secreto]--docker-server [servidor de inicio de sección] --docker-username [el nombre de usuario]--docker-password [la contraseña del registro. Elige cualquiera de las 2 disponibles]--docker-email [el email con el que esta enlazado el azure]

Nota: todos los datos que se encuentran en llaves se pueden encontrar en la pestaña Claves de Acceso del recurso ACR.

5°: En el mismo Cloud Shell, en el archivo .yaml (el manifesto de kubernetes) donde se clonó el api, le cambias el valor de donde dice image (en la sección de containers) a : [servidor de inicio de sesión]/[nombre de la imagen].

Luego, cambiamos el valor de campo name de ImagePullSecrets (Donde se registran en el manifesto los secretos) a el nombre del secreto que hemos creado hace poco.

Al hacer todo ello, guarda los cambios

6°: Finalmente, lanza la aplicación al Kubernetes con el comando:

kubectl apply -f [dirección exacta donde esta el manifesto .yaml]

Esperas a que termine y luego ve al terminal e ingresa kubectl get services o al panel de Kubernetes que aparece en el mismo recurso para ver la ip del nuevo servicio (eso toma 1 minuto en promedio para que aparezca) y lo puedas probar.
