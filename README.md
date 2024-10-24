
# EvacuationPlanningMonitoring API

This API Is Part of Interview Assigment From T. T. Software Solution. you can see detail from here [link](https://drive.google.com/file/d/1hNpO9VFaWXAueETuxELs0L2yn4YPNkHI/view).

## API Endpoints

### Evacuations
**Response Format:**

```json
{
    "data": null,
    "isSuccess": true,
    "statusCode": 200,
    "message": null,
    "dataTotalCount": 0,
    "error": null
}
```
#### 1. Get Evacuation Status

**Endpoint:**  
`GET /api/evacuations/status`

**Description:**  
Retrieves the current status of all evacuation zones.

**Response:**
```json
{
  "zoneID": "string",
  "totalEvacuated": "int",
  "evacuatingPeople": "int",
  "remainPeople": "int",
  "isEvacuatedComplete": "bool",
  "message": "string",
  "lastVehicleUsed": "string"
}
```

#### 2. Plan Evacuation

**Endpoint:**  
`POST /api/evacuations/plan`

**Description:**  
Creates and return a new evacuation plan and also.

**Response:**
```json
{
  "zoneID": "string",
  "vehicleID": "string",
  "eta": "string", //ex. "10 minutes"
  "numberOfPeople": "int",
  "message": "string",
}
```

#### 3. Update Status of Evacuation Plan

**Endpoint:**  
`PUT /api/evacuations/update`

**Description:**  
Updates an Status of existing evacuation plan.

**Request Body:**
```json
{
  "zoneID": "string",
  "evacuationPlanID": "string",
  "status": "string" 
}
```
**status**
```json
- READY
- INPROGRESS
- DONE
```

**Response:**
- `200 OK`: Success

#### 4. Clear Evacuation Plans

**Endpoint:**  
`DELETE /api/evacuations/clear`

**Description:**  
Clears all existing evacuation plans, zone and vehicles.

**Response:**
- `200 OK`: Success

### Evacuation Zones

#### 1. Create Evacuation Zone

**Endpoint:**  
`POST /api/evacuation-zones`

**Description:**  
Creates a new evacuation zone.

**Request Body:**
```json
[
  {
    "zoneID": "string",
    "location": {
      "latitude": 0,
      "longitude": 0
    },
    "description": "string"
  }
]
```

**Response:**
- `200 OK`: Success

### Vehicles

#### 1. Add Vehicles

**Endpoint:**  
`POST /api/vehicles`

**Description:**  
Adds a new vehicle to be used in the evacuation process.

**Request Body:**
```json
[
  {
    "vehicleID": "string",
    "type": "string",
    "capacity": 0,
    "status": "string"
  }
]
```

**Response:**
- `200 OK`: Success

#### 2. Vehicle Status

**Endpoint:**  
`GET /api/vehicles`

**Description:**  
Retrieves the current status of all vehicles.

**Response:**
```json
[
    {
      "vehicleID": "string",
      "capacity": 0,
      "type": "string",
      "locationCoordinates": {
        "latitude": 0,
        "longitude": 0
      },
      "speed": 0,
      "status": ""
    }
]
```

## Technologies Used
- **Swagger UI:** for API documentation and Integration testing.
- **Xunit:** for Unit testing.
- **C# .NET 6:** for API implementation.
- **Postgressql:** Database
- **Redis:** Caching
