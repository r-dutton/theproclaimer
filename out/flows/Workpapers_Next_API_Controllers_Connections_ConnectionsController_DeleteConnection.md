[web] DELETE /api/connections/{apiType}  (Workpapers.Next.API.Controllers.Connections.ConnectionsController.DeleteConnection)  [L36–L42] status=200
  └─ uses_service UserService
    └─ method GetUserId [L39]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
  └─ sends_request RemoveConnectionCommand [L39]
    └─ handled_by Workpapers.Next.Data.Commands.RemoveConnectionCommandHandler.Handle [L17–L87]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L48]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle
      └─ uses_service UnitOfWork
        └─ method Table [L52]
          └─ ... (no implementation details available)
      └─ uses_service ConnectionApiService
        └─ method GetApiMethods [L43]
          └─ implementation Workpapers.Next.ApplicationService.Features.Connections.ConnectionApiService.GetApiMethods [L20-L75]

