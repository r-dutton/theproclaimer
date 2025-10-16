[web] POST /api/binders/{binderId:Guid}/worksheets/{id:guid}/commands  (Workpapers.Next.API.Controllers.Workpapers.Worksheets.BinderWorksheetsController.AddWorksheetCommands)  [L377–L388] status=201 [auth=AuthorizationPolicies.User]
  └─ calls WorksheetRepository.WriteQuery [L381]
  └─ write Worksheet [L381]
    └─ reads_from Worksheets
  └─ uses_service IControlledRepository<Worksheet>
    └─ method WriteQuery [L381]
      └─ ... (no implementation details available)

