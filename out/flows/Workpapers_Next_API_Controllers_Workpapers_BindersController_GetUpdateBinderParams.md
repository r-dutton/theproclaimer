[web] GET /api/binders  (Workpapers.Next.API.Controllers.Workpapers.BindersController.GetUpdateBinderParams)  [L518–L539]
  └─ calls BinderColumnDataRepository.WriteQuery [L531]
  └─ writes_to BinderColumnData [L531]
    └─ reads_from BinderColumnData
  └─ queries BinderStatus [L523]
    └─ reads_from BinderStatuss
  └─ uses_service IControlledRepository<BinderColumnData>
    └─ method WriteQuery [L531]
  └─ uses_service IControlledRepository<BinderStatus> (AddScoped)
    └─ method ReadQuery [L523]

