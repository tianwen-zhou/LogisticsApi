
# Push Order OpenAPI Documentation

## Overview
This API is used to push customer orders to the logistics center system for further processing and delivery.

- **Base URL**: `https://api.xxx.co.nz`
- **Endpoint**: `/v1/orders/push`
- **Method**: `POST`
- **Request Format**: `JSON`
- **Response Format**: `JSON`

---

## Request Parameters

### Headers

| Parameter         | Type     | Required | Description                          |
|-------------------|----------|----------|--------------------------------------|
| `Authorization`   | `string` | Yes      | Authentication token, format: `Bearer <token>` |
| `Content-Type`    | `string` | Yes      | Fixed value: `application/json`      |

### Body

| Parameter            | Type     | Required | Description                                      |
|----------------------|----------|----------|--------------------------------------------------|
| `order_id`           | `string` | Yes      | Unique order identifier                          |
| `customer_name`      | `string` | Yes      | Customer name                                   |
| `customer_phone`     | `string` | Yes      | Customer contact phone number                   |
| `customer_email`     | `string` | No       | Customer email address                          |
| `delivery_address`   | `string` | Yes      | Delivery address                                |
| `items`              | `array`  | Yes      | List of items in the order                      |
| `items[].sku`        | `string` | Yes      | Product SKU                                     |
| `items[].name`       | `string` | Yes      | Product name                                    |
| `items[].quantity`   | `number` | Yes      | Product quantity                                |
| `items[].price`      | `number` | Yes      | Product unit price                              |
| `total_amount`       | `number` | Yes      | Total order amount                              |
| `order_time`         | `string` | Yes      | Order timestamp, format: `YYYY-MM-DD HH:mm:ss`  |

### Request Example

```json
{
  "order_id": "ORD123456",
  "customer_name": "John Doe",
  "customer_phone": "+64 21 123 4567",
  "customer_email": "john.doe@example.com",
  "delivery_address": "123 Queen Street, Auckland, New Zealand",
  "items": [
    {
      "sku": "SKU123",
      "name": "Wireless Mouse",
      "quantity": 2,
      "price": 25.99
    },
    {
      "sku": "SKU456",
      "name": "Bluetooth Keyboard",
      "quantity": 1,
      "price": 59.99
    }
  ],
  "total_amount": 111.97,
  "order_time": "2023-10-01 14:30:00"
}
```

---

## Response Parameters

### Response Body

| Parameter         | Type     | Description                                      |
|-------------------|----------|--------------------------------------------------|
| `code`            | `number` | Status code, `200` for success                  |
| `message`         | `string` | Response message                                |
| `data`            | `object` | Response data                                   |
| `data.order_id`   | `string` | Unique order identifier                         |
| `data.status`     | `string` | Order status, e.g., `RECEIVED`                  |

### Response Example

```json
{
  "code": 200,
  "message": "Order received successfully",
  "data": {
    "order_id": "ORD123456",
    "status": "RECEIVED"
  }
}
```

---

## Error Codes

| Status Code | Description                          |
|-------------|--------------------------------------|
| `400`       | Invalid request parameters          |
| `401`       | Authentication failed               |
| `500`       | Internal server error               |

---

## Example Call

### cURL Example

```bash
curl -X POST https://api.xxx.co.nz/v1/orders/push \
-H "Authorization: Bearer <your_token>" \
-H "Content-Type: application/json" \
-d '{
  "order_id": "ORD123456",
  "customer_name": "John Doe",
  "customer_phone": "+64 21 123 4567",
  "customer_email": "john.doe@example.com",
  "delivery_address": "123 Queen Street, Auckland, New Zealand",
  "items": [
    {
      "sku": "SKU123",
      "name": "Wireless Mouse",
      "quantity": 2,
      "price": 25.99
    },
    {
      "sku": "SKU456",
      "name": "Bluetooth Keyboard",
      "quantity": 1,
      "price": 59.99
    }
  ],
  "total_amount": 111.97,
  "order_time": "2023-10-01 14:30:00"
}'
```

---

## Version History

| Version  | Date       | Description           |
|----------|------------|-----------------------|
| v1.0.0   | 2023-10-01 | Initial release       |

---

### Notes
- Replace `<your_token>` with the actual authentication token.
