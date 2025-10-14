[web] GET /api/binders/{binderId:Guid}/worksheets  (Workpapers.Next.API.Controllers.Workpapers.Worksheets.BinderWorksheetsController.GetWorksheetParams)  [L437–L446] [auth=AuthorizationPolicies.User]
  └─ calls BinderRepository.ReadQuery [L439]
  └─ queries Binder [L439]
    └─ reads_from Binders
  └─ uses_service IControlledRepository<Binder>
    └─ method ReadQuery [L439]

