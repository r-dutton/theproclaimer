[web] DELETE /api/custom-statuses/binder/{id:Guid}  (Workpapers.Next.API.Controllers.CustomStatusesController.DeleteBinder)  [L169–L177] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ delete BinderStatus [L174]
    └─ reads_from BinderStatuss
  └─ write BinderStatus [L173]
    └─ reads_from BinderStatuss
  └─ uses_service IControlledRepository<BinderStatus> (AddScoped)
    └─ method WriteQuery [L173]
      └─ ... (no implementation details available)
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ BinderStatus writes=2 reads=0
    └─ services 1
      └─ IControlledRepository<BinderStatus>

