using GameRoot;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class ContactMonitor : MonoBehaviour
    {
        private List<Collider> _contacts = new List<Collider>();

        private void OnCollisionEnter(Collision collision)
        {
            if (!_contacts.Contains(collision.collider))
                _contacts.Add(collision.collider);
        }

        private void OnCollisionExit(Collision collision)
        {
            _contacts.Remove(collision.collider);
        }

        public bool IsOnValidSurface()
        {
            foreach (var contact in _contacts)
                if (contact.gameObject.CompareTag(Tags.Block) || contact.gameObject.CompareTag(Tags.Base))
                    return true;

            return false;
        }

        public void ClearContacts()
        {
            _contacts.Clear();
        }
    }
}