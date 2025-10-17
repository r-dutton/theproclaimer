[web] DELETE /api/connections/{apiType}  (Workpapers.Next.API.Controllers.Connections.ConnectionsController.DeleteConnection)  [L36–L42] status=200
  └─ uses_service UserService
    └─ method GetUserId [L39]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
        └─ uses_service User
          └─ method GetUserId [L67]
            └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
        └─ uses_service Guid?
          └─ method GetUserId [L64]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ sends_request RemoveConnectionCommand -> RemoveConnectionCommandHandler [L39]
    └─ handled_by Workpapers.Next.Data.Commands.RemoveConnectionCommandHandler.Handle [L17–L87]
      └─ uses_service UnitOfWork
        └─ method Table [L52]
          └─ implementation UnitOfWork.Table
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L48]
          └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
      └─ uses_service ConnectionApiService
        └─ method GetApiMethods [L43]
          └─ implementation Workpapers.Next.ApplicationService.Features.Connections.ConnectionApiService.GetApiMethods [L20-L75]
  └─ impact_summary
    └─ services 1
      └─ UserService
    └─ requests 1
      └─ RemoveConnectionCommand
    └─ handlers 1
      └─ RemoveConnectionCommandHandler
    └─ caches 1
      └─ IMemoryCache

