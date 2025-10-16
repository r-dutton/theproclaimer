[web] PUT /api/accounting/files/{id:Guid}/move  (Cirrus.Api.Controllers.Accounting.FilesController.Move)  [L260–L265] status=200 [auth=user]
  └─ calls FileRepository.WriteQuery [L263]
  └─ write File [L263]
    └─ reads_from Files
  └─ sends_request MoveFileCommand -> MoveFileCommandHandler [L264]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.MoveFileCommandHandler.Handle [L36–L118]
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L57]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
            └─ uses_service User
              └─ method GetUserId [L67]
                └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
            └─ uses_service Guid?
              └─ method GetUserId [L64]
                └─ ... (no implementation details available)
            └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L54]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
      └─ uses_service IControlledRepository<File> (Scoped (inferred))
        └─ method ReadQuery [L53]
          └─ implementation Cirrus.Data.Repository.Accounting.FileRepository.ReadQuery
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ File writes=1 reads=0
    └─ requests 1
      └─ MoveFileCommand
    └─ handlers 1
      └─ MoveFileCommandHandler
    └─ caches 1
      └─ IMemoryCache

