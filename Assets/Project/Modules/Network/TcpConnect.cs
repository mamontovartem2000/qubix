﻿using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine.Events;

public class TCPConnect
{
    public UnityAction<byte[]> GetMessage;
    public bool ConnectionIsReady { get; private set; } = false;

    private const int ConnectionTimedOut = 3000;

    private TcpClient _client;
    private Thread _clientListener;
    private NetworkStream _NS;
    private bool _working = true;

    private string _ip;
    private int _port;

    public TCPConnect(string ip, int port)
    {
        _ip = ip;
        _port = port;

        try
        {
            _client = new TcpClient();
            var result = _client.BeginConnect(_ip, _port, null, null);
            if (result.AsyncWaitHandle.WaitOne(ConnectionTimedOut, true))
            {
                _client.EndConnect(result);

                _clientListener = new Thread(ReceivingMessagesLoop);
                _clientListener.Start();
                ConnectionIsReady = true;
            }
            else
                CloseClient();
        }
        catch { CloseClient(); }
    }

    /// <summary>
    /// Сериализация и отправка TCP сообщения. Сначала отправляется размер сообщения в байтах, а потом уже сам текст.
    /// </summary>
    /// <param name="message">Текст сообщения.</param>
    public void SendMessage(string message)
    {
        try
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            byte[] sizeInByte = BitConverter.GetBytes(buffer.Length);

            _client.GetStream().Write(sizeInByte, 0, sizeInByte.Length);
            _client.GetStream().Write(buffer, 0, buffer.Length);
        }
        catch { TryStartReconnect(); }
    }

    public void SendMessage(byte[] message)
    {
        try
        {
            byte[] sizeInByte = BitConverter.GetBytes(message.Length);

            _client.GetStream().Write(sizeInByte, 0, sizeInByte.Length);
            _client.GetStream().Write(message, 0, message.Length);
        }
        catch { TryStartReconnect(); }
    }

    /// <summary>
    /// Приём TCP-потока с сервера с разделением потока на разные сообщения по байтам. 
    /// </summary>
    private void ReceivingMessagesLoop()
    {
        _NS = _client.GetStream();
        while (_working)
        {
            List<byte> buffer = new List<byte>();
            try
            {
                while (buffer.Count < 4)
                    GetByteFromStream(buffer);

                int mesCount = BitConverter.ToInt32(buffer.ToArray(), 0);
                buffer.Clear();

                while (buffer.Count < mesCount)
                    GetByteFromStream(buffer);
            }
            catch
            {
                TryStartReconnect();
                break;
            }

            GetMessage?.Invoke(buffer.ToArray());
        }
    }

    /// <summary>
    /// Проверка потока на наличие полученных байт и добавление их к _receivedBytesBuffer.
    /// </summary>
    private void GetByteFromStream(List<byte> buffer)
    {
        if (_NS.DataAvailable)
        {
            int ReadByte = _NS.ReadByte();
            if (ReadByte > -1)
            {
                buffer.Add((byte)ReadByte);
            }
        }
    }

    /// <summary>
    /// Закрытие сокетов, потока чтения и этого экземпляра, и вызов функции реконнекта.
    /// </summary>
    private void TryStartReconnect()
    {
        ConnectionIsReady = false;

        //TODO: Пока отключил возможность реконнкта.

        //if (ConnectionIsReady)
        //{
        //    ConnectionIsReady = false;
        //    CloseClient();
        //    Network.StartReconnect();
        //}

        //TODO: Наверное надо и else добавить?
    }


    public void CloseClient() //TODO: Добавить этот вызов на кнопку выхода из приложения
    {
        ConnectionIsReady = false;
        _working = false; //TODO: Переосмыслить все реконнекты и закрытия

        if (_client != null)
        {
            _client.LingerState = new LingerOption(true, 0); //Чтоб он не ожидал.
            _client.Close();
            _client = null;
        }

        if (_NS != null)
        {
            _NS.Close();
            _NS = null;
        }
    }

    ~TCPConnect()
    {
        CloseClient();
    }
}
