### RabbitMQ ile Headers Exchange yapısı kullanılarak oluşturulmuş Publisher-Consumer konsol uygulamasıdır.
### Dockerfile dosyaları içerisindeki ENV URI alanına RabbitMQ Cloud adresi yazılmalıdır.

### ./UdemyRabbitMQ.publisher
#### docker build -t headers-exc-pub-img .
#### docker run --name headers-exc-pub-con headers-exc-pub-img

### ./UdemyRabbitMQ.subscriber
#### docker build -t headers-exc-subs-img .
#### docker run --name headers-exc-subs-con headers-exc-subs-img
