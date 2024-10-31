using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH.AddressMS.Grpc;

namespace TH.CompanyMS.App
{
    public class AddressGrpcClientService
    {
        private readonly AddressProtoService.AddressProtoServiceClient _grpcClient;
        private readonly IMapper _mapper;
        public AddressGrpcClientService(AddressProtoService.AddressProtoServiceClient grpcClient, IMapper mapper)
        {
            _grpcClient = grpcClient ?? throw new ArgumentNullException(nameof(grpcClient));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<AddressViewReply> TrySaveAsync(AddressInputRequest request)
        {
            return await _grpcClient.TrySaveAddressAsync(request);
        }

        public async Task<AddresssListViewReply> TryFindAsync(AddressFilterRequest filter)
        {
            return await _grpcClient.TryGetAsync(filter);
        }
    }
}