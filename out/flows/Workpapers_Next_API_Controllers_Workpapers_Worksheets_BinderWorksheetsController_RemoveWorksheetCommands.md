[web] DELETE /api/binders/{binderId:Guid}/worksheets/{id:guid}/commands/{commandId:guid}  (Workpapers.Next.API.Controllers.Workpapers.Worksheets.BinderWorksheetsController.RemoveWorksheetCommands)  [L390–L400] status=200 [auth=AuthorizationPolicies.User]
  └─ calls WorksheetRepository.WriteQuery [L393]
  └─ write Worksheet [L393]
    └─ reads_from Worksheets
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ Worksheet writes=1 reads=0

