[web] DELETE /api/custom-statuses/binder/{id:Guid}  (Workpapers.Next.API.Controllers.CustomStatusesController.DeleteBinder)  [L165–L173] [auth=AuthorizationPolicies.Administrator]
  └─ writes_to BinderStatus [L170]
    └─ reads_from BinderStatuss
  └─ writes_to BinderStatus [L169]
    └─ reads_from BinderStatuss
  └─ uses_service IControlledRepository<BinderStatus> (AddScoped)
    └─ method WriteQuery [L169]

