helm install aws-eshop-client-app ./aws-eshop-client-app --set image.tag=10
helm status aws-eshop.service.catalog
helm upgrade aws-eshop-client-app ./ --set image.tag=11