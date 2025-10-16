[web] PUT /api/accounting/files/{id:Guid}/move  (Cirrus.Api.Controllers.Accounting.FilesController.Move)  [L260–L265] status=200 [auth=user]
  └─ calls FileRepository.WriteQuery [L263]
  └─ write File [L263]
    └─ reads_from Files
  └─ uses_service IControlledRepository<File>
    └─ method WriteQuery [L263]
      └─ ... (no implementation details available)
  └─ sends_request MoveFileCommand [L264]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.MoveFileCommandHandler.Handle [L36–L118]
      └─ uses_service IControlledRepository<File>
        └─ method ReadQuery [L53]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L54]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L57]
          └─ implementation IUserService.GetUserId [L18-L18]
          └─ ... (no implementation details available)

