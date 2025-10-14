[web] DELETE /api/binders/{binderId:Guid}/worksheets/{id:guid}/commands/{commandId:guid}  (Workpapers.Next.API.Controllers.Workpapers.Worksheets.BinderWorksheetsController.RemoveWorksheetCommands)  [L390–L400] [auth=AuthorizationPolicies.User]
  └─ calls WorksheetRepository.WriteQuery [L393]
  └─ writes_to Worksheet [L393]
    └─ reads_from Worksheets
  └─ uses_service IControlledRepository<Worksheet>
    └─ method WriteQuery [L393]

