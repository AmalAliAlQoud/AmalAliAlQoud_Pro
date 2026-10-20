import 'package:firstproj/loginpage.dart';
import 'package:flutter/material.dart';
import 'components/app_drawer.dart';
import 'profilepage.dart';

class HomePage extends StatelessWidget {
  const HomePage({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      drawer: const AppDrawer(),
      appBar: AppBar(
        title: const Text('First App'),
        backgroundColor: Colors.deepPurple,
      ),
      body: Container(
        width: double.infinity,
        height: double.infinity,
        decoration: const BoxDecoration(
          gradient: RadialGradient(
            colors: [Colors.deepPurple, Colors.black26],
            radius: 0.9,
          ),
        ),
        child: const Center(
          child: Text(
            'Home page',
            style: TextStyle(
              fontSize: 30,
              color: Colors.white,
              fontWeight: FontWeight.w700,
            ),
          ),
        ),
      ),
      bottomNavigationBar: Container(
        color: Colors.deepPurple,
        height: 80,
      ),
    );
  }
}