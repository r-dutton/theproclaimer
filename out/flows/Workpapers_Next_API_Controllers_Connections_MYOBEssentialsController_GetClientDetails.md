[web] PUT /api/connections/myob/es/clientdetails  (Workpapers.Next.API.Controllers.Connections.MYOBEssentialsController.GetClientDetails)  [L34–L40] status=200
  └─ sends_request GetBusinessQuery [L37]
    └─ handled_by Workpapers.Next.MyobEssentialsServices.Queries.GetBusinessQueryHandler.Handle [L13–L41]
      └─ uses_service IMapToNew<Business, ClientDetail>
        └─ method Map [L35]
          └─ ... (no implementation details available)
      └─ uses_service MyobEssentialsHttp
        └─ method GetHttpResponseAsync [L33]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L30]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle
      └─ uses_service UserService
        └─ method GetUserId [L30]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]

