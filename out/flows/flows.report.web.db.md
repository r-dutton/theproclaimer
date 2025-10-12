## Flow 1
- endpoint.controller: ReportsController.Get (web)
- validator: GetReportQueryValidator (app)
- cqrs.request: GetReportQuery (app)
- cqrs.handler: GetReportHandler (app)
- ef.entity: Report (data)
- db.table: Reports (db)

## Flow 2
- endpoint.controller: ReportsController.Get (web)
- cqrs.handler: GetReportHandler (app)
- ef.entity: Report (data)
- db.table: Reports (db)

