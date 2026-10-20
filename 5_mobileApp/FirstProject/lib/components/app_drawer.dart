import 'package:flutter/material.dart';

import '../homepage.dart';
import '../loginpage.dart'; // استيراد صفحة تسجيل الدخول
import '../profilepage.dart';

class AppDrawer extends StatelessWidget {
  const AppDrawer({super.key});

  @override
  Widget build(BuildContext context) {
    return Drawer(
      child: Column(
        children: [
          Expanded(
            child: ListView(
              padding: EdgeInsets.zero,
              children: [
                const UserAccountsDrawerHeader(
                  accountName: Text('Amal AlQoud'),
                  accountEmail: Text('amalalqoud@live.com'),
                  currentAccountPicture: CircleAvatar(
                    backgroundColor: Colors.white,
                    child: Text(
                      'A',
                      style: TextStyle(
                        fontSize: 24,
                        color: Colors.purple,
                      ),
                    ),
                  ),
                  decoration: BoxDecoration(
                    color: Colors.purple,
                  ),
                ),
                ListTile(
                  leading: const Icon(Icons.home),
                  title: const Text('Home'),
                  subtitle: const Text('Go to Home screen'),
                  trailing: const Icon(Icons.arrow_forward_ios),
                  onTap: () {
                    Navigator.pop(context);
                    Navigator.push(
                      context,
                      MaterialPageRoute(
                        builder: (context) => const HomePage(),
                      ),
                    );
                  },
                ),
                const Divider(),
                ListTile(
                  leading: const Icon(Icons.settings),
                  title: const Text('Profile'),
                  subtitle: const Text('Go to Home settings profile'),
                  trailing: const Icon(Icons.arrow_forward_ios),
                  onTap: () {
                    Navigator.pop(context);
                    Navigator.push(
                      context,
                      MaterialPageRoute(
                        builder: (context) => const ProfilePage(),
                      ),
                    );
                  },
                ),
                const Divider(),
              ],
            ),
          ),

          // عنصر تسجيل الخروج في أقصى أسفل الـ Drawer
          const Divider(),
          ListTile(
            leading: const Icon(Icons.logout, color: Colors.red),
            title: const Text(
              'Logout',
              style: TextStyle(
                color: Colors.red,
                fontWeight: FontWeight.bold,
              ),
            ),
            onTap: () {
              Navigator.pop(context);
              Navigator.pushAndRemoveUntil(
                context,
                MaterialPageRoute(
                  builder: (context) => const LoginPage(),
                ),
                    (route) => false,
              );
            },
          ),
          const SizedBox(height: 10),
        ],
      ),
    );
  }
}