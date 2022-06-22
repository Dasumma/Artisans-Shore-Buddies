using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Relay;
using Unity.Services.Relay.Http;
using Unity.Services.Relay.Models;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport;
using Unity.Networking.Transport.Relay;
using Unity.Services.Core.Environments;
using NetworkEvent = Unity.Networking.Transport.NetworkEvent;

public class RelayManager : Singleton <RelayManager>
{
    public struct RelayHostData
    {
        public string JoinCode;
        public string IPv4Address;
        public ushort Port;
        public Guid AllocationID;
        public byte[] AllocationIDBytes;
        public byte[] ConnectionData;
        public byte[] Key;
	}
	public struct RelayJoinData
	{
        public string JoinCode;
		public string IPv4Address;
		public ushort Port;
		public Guid AllocationID;
		public byte[] AllocationIDBytes;
		public byte[] ConnectionData;
		public byte[] HostConnectionData;
		public byte[] Key;
	}
    [SerializeField]
    private string environment = "production";

    [SerializeField]
    private int maxConnections = 5;

    public bool IsRelayEnabled => Transport != null&&
        Transport.Protocol == UnityTransport.ProtocolType.RelayUnityTransport;
    public UnityTransport Transport =>NetworkManager.Singleton.gameObject.GetComponent<UnityTransport>();

    public async Task<RelayHostData> SetupRelay()
    {
        InitializationOptions options = new InitializationOptions()
            .SetEnvironmentName(environment);
            await UnityServices.InitializeAsync(options);
        if(!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
        Allocation allocation = await Relay.Instance.CreateAllocationAsync(maxConnections);
        RelayHostData relayHostData = new RelayHostData
        {
            Key = allocation.Key,
            Port = (ushort)allocation.RelayServer.Port,
            AllocationID = allocation.AllocationId,
            AllocationIDBytes = allocation.AllocationIdBytes,
            IPv4Address = allocation.RelayServer.IpV4,
            ConnectionData = allocation.ConnectionData

        };
        relayHostData.JoinCode = await Relay.Instance.GetJoinCodeAsync(relayHostData.AllocationID);
        Transport.SetRelayServerData(relayHostData.IPv4Address, relayHostData.Port,relayHostData.AllocationIDBytes,
        relayHostData.Key,relayHostData.ConnectionData);
        Logger.Instance.LogInfo($"Relay Server generate a join code{relayHostData.JoinCode}");
        return relayHostData;
    }
        public async Task<RelayJoinData> JoinRelay(string joinCode)
    {
        Logger.Instance.LogInfo($"Client Joining Game With Join Code: {joinCode}");

        InitializationOptions options = new InitializationOptions()
            .SetEnvironmentName(environment);

        await UnityServices.InitializeAsync(options);

        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        JoinAllocation allocation = await Relay.Instance.JoinAllocationAsync(joinCode);

        RelayJoinData relayJoinData = new RelayJoinData
        {
            Key = allocation.Key,
            Port = (ushort)allocation.RelayServer.Port,
            AllocationID = allocation.AllocationId,
            AllocationIDBytes = allocation.AllocationIdBytes,
            ConnectionData = allocation.ConnectionData,
            HostConnectionData = allocation.HostConnectionData,
            IPv4Address = allocation.RelayServer.IpV4,
            JoinCode = joinCode
        };

        Transport.SetRelayServerData(relayJoinData.IPv4Address, relayJoinData.Port, relayJoinData.AllocationIDBytes,
            relayJoinData.Key, relayJoinData.ConnectionData, relayJoinData.HostConnectionData);

        Logger.Instance.LogInfo($"Client Joined Game With Join Code: {joinCode}");

        return relayJoinData;
    }
}

