# HTTP Status Code Capture

The analyzer now captures HTTP status codes for controller endpoints and (by default inference) will display them in flow headers.

## Controller Endpoints
Status codes are collected from:
- [ProducesResponseType] attributes (any integer or StatusCodes constants parsed).
- MVC return helper methods invoked inside `return` statements (`Ok`, `Created`, `CreatedAtAction`, `CreatedAtRoute`, `NoContent`, `BadRequest`, `Unauthorized`, `Forbidden`, `NotFound`, `Conflict`, `Problem`).
- Fallback inference if none explicitly found: `201` for POST actions, otherwise `200`.

The collected codes are emitted on `endpoint.controller` nodes as a property:

```json
{
  "type": "endpoint.controller",
  "props": {
    "route": "/api/reports",
    "http_method": "POST",
    "status_codes": [201]
  }
}
```

## Flow Output
When present, status codes appear in the endpoint header line:

```
[web] POST /api/reports  (Sample.Web.Controllers.ReportsController.Create)  [L56–L76] status=201
```
Multiple codes are comma separated (e.g. `status=200,404`).

## Minimal API Endpoints
(Current) Minimal API endpoints do not yet infer status codes. Planned parity: default 201 for POST, else 200. (See TODO) 

## Notes
- Codes are de-duplicated and sorted.
- Only surface if at least one code is found/inferred.
- Future enhancement: capture explicit numeric `StatusCode(int, ...)` calls.
