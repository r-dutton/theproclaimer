[web] DELETE /api/connections/{apiType}  (Workpapers.Next.API.Controllers.Connections.ConnectionsController.DeleteConnection)  [L36–L42]
  └─ uses_service UserService
    └─ method GetUserId [L39]
  └─ sends_request RemoveConnectionCommand [L39]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.Data.Commands.RemoveConnectionCommandHandler.Handle [L17–L87]
      └─ uses_service ConnectionApiService
        └─ method GetApiMethods [L43]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L48]
      └─ uses_service UnitOfWork
        └─ method Table [L52]

